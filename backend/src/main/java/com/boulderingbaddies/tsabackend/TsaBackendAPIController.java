package com.boulderingbaddies.tsabackend;

import jakarta.transaction.Transactional;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.hateoas.CollectionModel;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;


import java.net.URI;
import java.util.ArrayList;
import java.util.List;

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
            value = "/create/waittime",
            consumes = {MediaType.APPLICATION_JSON_VALUE},
            produces = {MediaType.APPLICATION_JSON_VALUE})
    public ResponseEntity<WaitTime> createUser(@RequestBody WaitTime reqWaitTime) {
        WaitTime persistedPerson = waitTimeRepository.save(reqWaitTime);
        return ResponseEntity
                .created(URI
                        .create(String.format("/wait_time/%s", reqWaitTime.getCreatedAt())))
                .body(persistedPerson);
    }
}
