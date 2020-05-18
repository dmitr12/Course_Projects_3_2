package A1.A2.A3;

import java.util.Date;
import A1.A2.*;

public class AAA implements Student {

    String surname;
    String name;
    String fathername;
    String universityName;
    Date birthday;
    Date firstDate;

    public void setSurname(String surname){this.surname=surname;}
    public void setName(String name){this.name=name;}
    public void setFathername(String fathername){this.fathername=fathername;}
    public void setBirthday(int yyyy, int mm, int dd){
        if(yyyy>Limityyyy) birthday=new Date(yyyy,mm,dd);
        else System.out.println("Нарушено ограничение по году");
    }
    public void setFirstDate(Date d){firstDate=d;}
    public void setUniversity(String name){
        if(name.length()<LimitUniversityLength) universityName=name;
        else System.out.println("Нарушено ограничение");
    }
    public String getSurname(){return surname;}
    public String getName(){return name;}
    public String getFathername(){return fathername;}
    public String getUniversity(){return universityName;}
    public Date getFirstDate(){return firstDate;}
    public Date getBirthday(){return birthday;}
}
