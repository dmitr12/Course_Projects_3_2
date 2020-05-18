
import pack.CBean;

import javax.servlet.RequestDispatcher;
import javax.servlet.Servlet;
import javax.servlet.ServletContext;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;
import java.io.IOException;
import java.io.PrintWriter;

public class Ccc extends HttpServlet implements Servlet {
    CBean Cb;

    public void init() throws ServletException {
        super.init();
    }

    protected void service(HttpServletRequest rq, HttpServletResponse rs) throws ServletException, IOException {
//        ----------REQUEST----------
        CBean R=(CBean)rq.getAttribute("attrCBean");
        if(R==null){
            R=new CBean();
            R.setValue1("1");
            R.setValue2("2");
            R.setValue3("3");
            rq.setAttribute("attrCBean", R);
        }
//        -----------SESSION--------------
        HttpSession session=rq.getSession();
        CBean S=(CBean)session.getAttribute(session.getId());
        if(S==null){
            S=new CBean();
            S.setValue1("1");
            S.setValue2("2");
            S.setValue3("3");
            session.setAttribute(session.getId(), S);
//            session.setMaxInactiveInterval(5);
        }
        RequestDispatcher rd=rq.getRequestDispatcher("/jsp/Ccc.jsp");
        rd.forward(rq,rs);
    }
}
