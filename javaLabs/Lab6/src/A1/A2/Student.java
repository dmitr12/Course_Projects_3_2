package A1.A2;

import A1.*;
import java.util.Date;

public interface Student extends Person {
    int LimitUniversityLength=100;
    void setFirstDate(Date d);
    Date getFirstDate();
    void setUniversity(String u);
    String getUniversity();

}
