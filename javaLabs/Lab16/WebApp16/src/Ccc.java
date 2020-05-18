
import pack.CBean;

import javax.servlet.RequestDispatcher;
import javax.servlet.Servlet;
import javax.servlet.ServletContext;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.io.PrintWriter;

public class Ccc extends HttpServlet implements Servlet {
    CBean Cb;

    public void init() throws ServletException {
        super.init();
        Cb=new CBean();
        ServletContext sc=getServletContext();
        sc.setAttribute("attrCBean",Cb);
    }

    protected void service(HttpServletRequest rq, HttpServletResponse rs) throws ServletException, IOException {
        ServletContext sc=getServletContext();
        String cbean=rq.getParameter("CBean");
        String value1=rq.getParameter("Value1");
        String value2=rq.getParameter("Value2");
        String value3=rq.getParameter("Value3");
        if(cbean!=null) {
            if(cbean.equals("new"))
            {
                Cb=new CBean();
                if(value1!=null)
                    Cb.setValue1(value1);
                if(value2!=null)
                    Cb.setValue2(value2);
                if(value3!=null)
                    Cb.setValue2(value3);
                sc.setAttribute("attrCBean",Cb);
            }
            else
            {
                if(value1!=null)
                    Cb.setValue1(value1);
                if(value2!=null)
                    Cb.setValue2(value2);
                if(value3!=null)
                    Cb.setValue2(value3);
            }
        }
        else{
            if(value1!=null)
                Cb.setValue1(value1);
            if(value2!=null)
                Cb.setValue2(value2);
            if(value3!=null)
                Cb.setValue3(value3);
        }
        RequestDispatcher rd=rq.getRequestDispatcher("/jsp/Ccc.jsp");
        rd.forward(rq,rs);
    }
}
