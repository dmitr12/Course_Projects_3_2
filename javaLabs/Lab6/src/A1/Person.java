package A1;

import java.util.Date;

public interface Person {
    int Limityyyy=1900;
    void setSurname(String surname);
    void setName(String name);
    void setFathername(String fathername);
    void setBirthday(int yyyy, int mm, int dd);
    String getSurname();
    String getName();
    String getFathername();
    Date getBirthday();
}
