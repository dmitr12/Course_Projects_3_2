<?xml version="1.0" encoding="ISO-8859-1" ?>
<%@ page language="java" contentType="text/html; charset=ISO-8859-1" pageEncoding="ISO-8859-1" %>
<%@ page import="pack.CBean" %>
<%@ page import="java.util.*" %>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1" />
    <title>jspTask3</title>
</head>
<body>
<% ServletContext sc=pageContext.getServletContext();
        CBean cBean=(CBean)sc.getAttribute("attrCBean");%>
<%=sc.getAttribute("attrCBean")%><br/>
Value1:<%=cBean.getValue1()%><br/>
Value2:<%=cBean.getValue2()%><br/>
Value3:<%=cBean.getValue3()%>
</body>
</html>