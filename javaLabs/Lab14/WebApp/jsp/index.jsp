<%@ page language="java" contentType="text/html;charset=ISO-8859-1"%>
<%@ page import="jspclass.*"%>
        <head>
        <meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1">
        <%! String name=null;
         Reg r=new Reg();
         Integer n=r.GetHOUR();
          protected String Salutation(Integer h){
          String rc="Good ";
          if((h>0)&&(h<=5))
               rc+="night";
          else if((h>5)&&(h<=12))
               rc+="morning";
          else if((h>12)&&(h<=17))
               rc+="afternoon";
          else
               rc+="evening";
          return rc;
          }%>
        </head>
        <body>
        <%=Salutation(n)%>
        </body>