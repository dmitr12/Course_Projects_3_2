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
<%--------------REQUEST-------------%>
REQUEST<br/>
<%  System.out.println("Ccc.jsp");
    CBean cBeanR=(CBean)request.getAttribute("attrCBean");%>
Attribute: 'attrCBean':
<%=request.getAttribute("attrCBean")%><br/>
Value1:<%=cBeanR.getValue1()%><br/>
Value2:<%=cBeanR.getValue2()%><br/>
Value3:<%=cBeanR.getValue3()%><br/>
<%-----------------SESSION-------------------%>
SESSION<br/>
<% CBean cBeanS=(CBean)request.getSession().getAttribute(request.getSession().getId());%>
Attribute '<%=request.getSession().getId()%>':
<%=request.getSession().getAttribute(request.getSession().getId())%><br/>
Value1:<%=cBeanS.getValue1()%><br/>
Value2:<%=cBeanS.getValue2()%><br/>
Value3:<%=cBeanS.getValue3()%>
</body>
</html>