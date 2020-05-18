import javax.servlet.RequestDispatcher;
import javax.servlet.Servlet;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.io.PrintWriter;

public class Jjj extends HttpServlet implements Servlet{
    public Jjj(){
        super();
    }

    protected void service(HttpServletRequest rq, HttpServletResponse rs) throws IOException, ServletException {
        super.service(rq,rs);
    }

    protected void doGet(HttpServletRequest rq, HttpServletResponse rs) throws IOException, ServletException {
        String hour=rq.getParameter("hour");
        int h;
        try{
            h=new Integer(hour);
        } catch (NumberFormatException e){
            h=-1;
        }
        rs.setContentType("text/html");
        PrintWriter pw=rs.getWriter();
        String rc="Good ";
        if((h>0)&&(h<=5))
            rq.getRequestDispatcher("/javaClasses/jsp/night.jsp").forward(rq,rs);
        else if((h>5)&&(h<=12))
            rq.getRequestDispatcher("/javaClasses/jsp/morning.jsp").forward(rq,rs);
        else if((h>12)&&(h<=17))
            rq.getRequestDispatcher("/javaClasses/jsp/afternoon.jsp").forward(rq,rs);
        else
            rq.getRequestDispatcher("/javaClasses/jsp/evening.jsp").forward(rq,rs);
        pw.println(rc);
        pw.flush();
    }
}