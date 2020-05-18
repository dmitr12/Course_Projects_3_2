<%@ page import="java.util.Enumeration" %>
<?xml version="1.0" encoding="ISO-8859-1" ?>
<%@ page language="java" contentType="text/html; charset=ISO-8859-1" pageEncoding="ISO-8859-1" %>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1" />
    <title>jspTask3</title>
</head>
<body>
<% ServletContext sc=pageContext.getServletContext();
    Enumeration en=sc.getInitParameterNames();
    String param="";
    while(en.hasMoreElements()){
        param=(String)en.nextElement();%>
<br/><%=param+"="%><%=sc.getInitParameter(param)%>
<%}%>
</body>
</html>