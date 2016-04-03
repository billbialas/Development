package com.company;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by wbialas on 3/8/2016.
 */
public class Monster implements ISaveable {
    private String monsterName;
    private int hitPoints;
    private int strength;

    public Monster(String monsterName, int hitPoints, int strength) {
        this.monsterName = monsterName;
        this.hitPoints = hitPoints;
        this.strength = strength;
    }

    public String getMonsterName() {
        return monsterName;
    }

    public int getHitPoints() {
        return hitPoints;
    }

    public int getStrength() {
        return strength;
    }

    @Override
    public String toString() {
        return "Monster{" +
                "monsterName='" + monsterName + '\'' +
                ", hitPoints=" + hitPoints +
                ", strength=" + strength +
                '}';
    }

    @Override
    public List<String> write() {
        List<String> values = new ArrayList<String>();
        values.add(0,this.monsterName);
        values.add(1,"" + this.hitPoints);
        values.add(2,"" + this.strength);

        return values;
    }

    @Override
    public void read(List<String> savedValues) {
        if (savedValues !=null && savedValues.size() >0){
            this.monsterName=savedValues.get(0);
            this.hitPoints=Integer.parseInt((savedValues.get(1)));
            this.strength=Integer.parseInt((savedValues.get(2)));

        }

    }
}
