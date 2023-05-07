package com.boulderingbaddies.tsabackend;


public class Airport {

    private String code;
    private String name;
    private int terminals;

    public Airport(String code, String name, int terminals) {
        this.code = code;
        this.name = name;
        this.terminals = terminals;
    }

    public String getCode() {
        return code;
    }

    public String getName() {
        return name;
    }

    public int getTerminals() {
        return terminals;
    }
}