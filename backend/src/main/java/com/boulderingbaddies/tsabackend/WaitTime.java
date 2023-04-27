package com.boulderingbaddies.tsabackend;

import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;

@Entity
public class WaitTime {

    @Id
    @GeneratedValue(strategy=GenerationType.AUTO)
    private Long id;

    // Total Elapsed Time in Seconds
    private Double elapsedTime;

    // When was this record posted
    private String createdAt;

    // 3-Digit Airport Code + Terminal Code Combination (Ex: SNA-A)
    private String terminal;

    protected WaitTime() {}

    public WaitTime(Double elapsedTime, String createdAt, String terminal) {
        this.elapsedTime = elapsedTime;
        this.createdAt = createdAt;
        this.terminal = terminal;
    }

    public Long getId() {
        return id;
    }

    public Double getElapsedTime() {
        return elapsedTime;
    }

    public String getCreatedAt() {
        return createdAt;
    }

    public String getTerminal() {
        return terminal;
    }
}