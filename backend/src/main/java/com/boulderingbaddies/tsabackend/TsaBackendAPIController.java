package com.boulderingbaddies.tsabackend;

import com.fasterxml.jackson.databind.ObjectMapper;
import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;
import jakarta.transaction.Transactional;
import okhttp3.Call;
import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.Response;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.hateoas.CollectionModel;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.io.IOException;
import java.io.InputStreamReader;
import java.io.Reader;
import java.net.*;
import java.text.SimpleDateFormat;
import java.util.*;

import org.json.simple.JSONArray;
import org.json.simple.JSONObject;
import org.json.simple.parser.JSONParser;
import java.io.FileReader;

@RestController
public class TsaBackendAPIController {

    @RequestMapping("/goodbye")
    public String goodbye() {
        return "Goodbye from Bouldering Baddies";
    }

    @Autowired
    TerminalRepository terminalRepo;

    @Autowired
    WaitTimeRepository waitTimeRepository;

    @RequestMapping("/terminals")
    public @ResponseBody ResponseEntity<?> getAllTerminals() {
        Iterable<Terminal> terminals = terminalRepo.findAll();
        if(terminals == null) return ResponseEntity.status(HttpStatus.NOT_FOUND).body(null);
        CollectionModel<Terminal> resources = CollectionModel.of(terminals);
        return ResponseEntity.ok(resources);
    }

    @RequestMapping("/waittimes")
    public @ResponseBody ResponseEntity<?> getAllWaitTimes() {
        Iterable<WaitTime> waittimes = waitTimeRepository.findAll();
        if(waittimes == null) return ResponseEntity.status(HttpStatus.NOT_FOUND).body(null);
        CollectionModel<WaitTime> resources = CollectionModel.of(waittimes);
        return ResponseEntity.ok(resources);
    }

    // Save 3 Sample Terminals for SNA
    @PostMapping("/create/terminals")
    @Transactional
    public @ResponseBody ResponseEntity<?> createTerminal() {
        Terminal snaA = new Terminal("SNA", "A");
        Terminal snaB = new Terminal("SNA", "B");
        Terminal snaC= new Terminal("SNA", "C");

        terminalRepo.save(snaA);
        terminalRepo.save(snaB);
        terminalRepo.save(snaC);

        List<Terminal> savedTerminals = new ArrayList<Terminal>();
        savedTerminals.add(snaA);
        savedTerminals.add(snaB);
        savedTerminals.add(snaC);

        return new ResponseEntity<>(savedTerminals, HttpStatus.CREATED);
    }

    @PostMapping(
            path = "/create/waittime",
            consumes = {MediaType.APPLICATION_JSON_VALUE},
            produces = {MediaType.APPLICATION_JSON_VALUE})
    public ResponseEntity<WaitTime> createUser(@RequestBody WaitTime reqWaitTime) {
        WaitTime persistedPerson = waitTimeRepository.save(reqWaitTime);
        return ResponseEntity
                .created(URI
                        .create(String.format("/wait_time/%s", reqWaitTime.getCreatedAt())))
                .body(persistedPerson);
    }

    // Auto-generate wait times for a given airport
    // We will generate 3 data points for each terminal
    // Created 2 days ago, spaced 12 hours between each other
    // Ex: JFK
    @PostMapping(value = {"/autogen/{airport}"})
    public ResponseEntity<ArrayList<WaitTime>> autogenWaitTimes(@PathVariable(required=true,name="airport") String airport) {

        JSONParser parser = new JSONParser();
        ArrayList<WaitTime> waitTimes = new ArrayList<>();
        try {

            ArrayList<Airport> airports = getAirports();
            int numTerminals = 0;
            for (Airport currentAirport: airports) {
                if (currentAirport.getCode().equalsIgnoreCase(airport)) {
                    numTerminals = currentAirport.getTerminals();
                    break;
                }
            }

            // Terminal has the format airportCode-Terminal#
            // Ex: JFK-1
            // Generate 3 estimated wait times for each terminal
            for (int i = 0; i < numTerminals; i++) {

                // Generate AirportCode-Terminal#
                int terminalNumber = i + 1;
                String airportCode = airport +  "-" + terminalNumber;

                // Generate 3 data points
                // Requirements:
                // 1) Generated 2 days ago
                // 2) Spaced out 6 hours from each other
                // 3) Wait time ranging from 30 min - 3 hours (converted to seconds)

                // Get the date from 2 days ago
                SimpleDateFormat f = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss.SSS'Z'");
                f.setTimeZone(TimeZone.getTimeZone("UTC"));
                Calendar cal = Calendar.getInstance();
                cal.add(Calendar.DATE, -2);

                // Generate 3 createdAt times
                String firstCreatedAt = f.format(cal.getTime());
                cal.add(Calendar.HOUR, 6);
                String secondCreatedAt = f.format(cal.getTime());
                cal.add(Calendar.HOUR, 6);
                String thirdCreatedAt = f.format(cal.getTime());

                // Generate 3 elapsed times
                // Anywhere between 1800 and 10800 seconds
                Random r = new Random();
                int low = 1800;
                int high = 110800;

                int firstElapsedTime = r.nextInt(high-low) + low;
                int secondElapsedTime = r.nextInt(high-low) + low;
                int thirdElapsedTime = r.nextInt(high-low) + low;

                // Generate and save 3 wait times for the terminal
                WaitTime firstWaitTime = new WaitTime((double)firstElapsedTime, firstCreatedAt, airportCode);
                WaitTime secondWaitTime = new WaitTime((double)secondElapsedTime, secondCreatedAt, airportCode);
                WaitTime thirdWaitTime = new WaitTime((double)thirdElapsedTime, thirdCreatedAt, airportCode);

                waitTimes.add(firstWaitTime);
                waitTimes.add(secondWaitTime);
                waitTimes.add(thirdWaitTime);
            }
        } catch (Exception e) {
            e.printStackTrace();
            WaitTime errWaitTime = new WaitTime(0.0, e.getMessage(), e.getLocalizedMessage());
            waitTimes.add(errWaitTime);
            return ResponseEntity
                    .created(URI
                            .create(String.format("/wait_time/%s", waitTimes.get(0).getCreatedAt())))
                    .body(waitTimes);
        }

        waitTimeRepository.saveAll(waitTimes);
        return ResponseEntity
                .created(URI
                        .create(String.format("/wait_time/%s", waitTimes.get(0).getCreatedAt())))
                .body(waitTimes);
    }

    public ArrayList<Airport> getAirports() throws IOException {

        ArrayList<Airport> airports = new ArrayList<>();

        Request request = new Request.Builder()
                .url("http://ec2-18-119-130-187.us-east-2.compute.amazonaws.com:8080/airports.json")
                .build();

        OkHttpClient client = new OkHttpClient();
        Call call = client.newCall(request);
        Response response = call.execute();

        String json = response.body().string();

        // convert JSON string to Map
        Gson gson = new Gson();
        Airports rootAirports = gson.fromJson(json, Airports.class);
        List<Airport> airportsList = rootAirports.airports;
        for (int i = 0; i < airportsList.toArray().length; i++) {
            Airport current = airportsList.get(i);
            airports.add(current);
        }

        return airports;
    }
}
