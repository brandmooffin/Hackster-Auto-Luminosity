package apps.brokenwallsstudios.autoluminosity.model;

/**
 * Created by Brandon on 1/15/2016.
 */
public class LifxLight {
    public String id;
    public String uuid;
    public String label;
    public boolean connected;
    public String power;
    //color
    public int brightness;
    public LifxGroup group;
    //location
    //product
    public String last_seen;
    public double seconds_since_seen;
}
