package com.company;

import A1.A2.A3.A4.*;
import A1.*;
import A1.A2.A3.*;
import A1.A2.*;
import B1.B2.*;
import B1.*;
import B1.B2.B3.*;
import B1.B2.B3.B4.*;

public class Main {

    public static void main(String[] args) {
        FA fa=new FA(4);
        FAA faa=new FAA(3);
        SAA saa=new SAA(5);
        FAAA faaa=new FAAA(7);
        FAAAA faaaa=new FAAAA(8);
        SAAAA saaaa=new SAAAA(10);
        fa.display();faa.display();saa.display();faaa.display();faaaa.display();saaaa.display();
        FB fb=new FB(89);
        FBB fbb=new FBB(67);
        SBB sbb=new SBB(189);
        FBBB fbbb=new FBBB(34);
        FBBBB fbbbb=new FBBBB(23);
        SBBBB sbbbb=new SBBBB(21);
        fb.display();fbb.display();sbb.display();fbbb.display();fbbbb.display();sbbbb.display();
        SB sb=new SB(6,fa);
        sb.MethFA();
        SA sa=new SA(78);
        sa.Check();
    }
}
