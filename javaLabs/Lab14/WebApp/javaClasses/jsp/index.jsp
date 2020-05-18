
<%@ page import="java.util.*" %>
<%@ page import="java.text.SimpleDateFormat" %>
<%@ page language="java" contentType="text/html;charset=iso-8859-1"%>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1">
    <%!Calendar C =Calendar.getInstance();
        String name=null;
        Date date=new Date();
        Integer n=GetHOUR();
        protected String Salutation(Integer h){
            String rc="";
            if((h>0)&&(h<=5))
                rc="night.jsp";
            else if((h>5)&&(h<=12))
                rc="morning.jsp";
            else if((h>12)&&(h<=17))
                rc="afternoon.jsp";
            else
                rc="evening.jsp";
            return rc;}
        public Integer GetHOUR() {
            return ((Integer)C.get(Calendar.HOUR_OF_DAY));
        }
        public String getD(){
            Date date=new Date();
            SimpleDateFormat formatDt=new SimpleDateFormat("dd.MM.yy");
            Calendar clndr=Calendar.getInstance();
            String rows="";
            for(int i=0;i<7;i++){
                clndr.setTime(date);
                clndr.add(Calendar.DATE,i);
                rows+="<tr><td>"+formatDt.format(clndr.getTime())+" -</td><td>"+
                        clndr.get(clndr.DAY_OF_WEEK)+"</td></tr>";
            }
            return rows;
        }
    %>
</head>
<body>
<%--<jsp:include page = "/Jjj" >--%>
<%--    <jsp:param name="hour" value="<%=GetHOUR().toString()%>" />--%>
<%--</jsp:include>--%>
<%--<br>--%>
<jsp:include page="<%=Salutation(n)%>"/>
<br>
<jsp:include page="/Afternoon"/>
<br>
<%--<jsp:forward page="/Afternoon"/>--%>
<table>
    <tbody>
    <%=getD()%>
    </tbody>
</table>
</body>