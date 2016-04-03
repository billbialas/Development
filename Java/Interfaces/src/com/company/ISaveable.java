package com.company;

import java.util.List;

/**
 * Created by wbialas on 3/8/2016.
 */
public interface ISaveable {
    List<String> write();
    void read(List<String> savedValues);
}
