package com.boulderingbaddies.tsabackend;


import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.*;

import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@Controller
public class TsaBackendController {
    private long startTime = 0;
    private long elapsedTime = 0;
    private long mostRecent = 0;
    private long totalTime = 0;
    private int count = 0;

    @GetMapping("/")
    public String index(Model model) {
        model.addAttribute("meanTime", getMeanTime());
        model.addAttribute("mostRecent", getRecentTime());
        model.addAttribute("elapsedTime", elapsedTime);

        return "timer";
    }

    @PostMapping("/start")
    public String startTimer() {
        startTime = System.currentTimeMillis();
        return "redirect:/";
    }

    @PostMapping("/stop")
    public String stopTimer() {
        if (startTime != 0) {
            elapsedTime = System.currentTimeMillis() - startTime;
            totalTime += elapsedTime;
            count++;
            startTime = 0;
        }
        return "redirect:/";
    }

    @ModelAttribute("elapsedTime")
    public String getElapsedTime() {
        long seconds = elapsedTime / 1000;
        long milliseconds = elapsedTime % 1000;
        return String.format("%d.%03d", seconds, milliseconds);
    }

    private String getMeanTime() {
        if (count == 0) {
            return "No times recorded yet";
        } else {
            long meanTime = totalTime / count;
            return String.format("%d seconds, %d milliseconds",
                    meanTime / 1000, meanTime % 1000);
        }
    }

    private String getRecentTime(){

        if (count == 0) {
            return "Please input data";
        } else {

            return String.format("\nThe most recent time was \n %d seconds, %d milliseconds",
                    mostRecent / 1000, mostRecent % 1000);
        }

    }


    

    @RequestMapping("/seth")
    public String seth() {
        return "Did I do A3 right?";
    }

    @RequestMapping("/joshua")
    public String joshua() {
        return "It would be hilarious if I messed up Seth's commit";
    }

}
