package apps.brokenwallsstudios.autoluminosity.model;

import java.util.ArrayList;

/**
 * Created by brand on 1/8/2016.
 */
public class Room {
    public int Id;
    public int UserId;
    public String Name;
    public String CreateDate;
    public String ExternalId;
    public ArrayList<Light> Lights;
}
