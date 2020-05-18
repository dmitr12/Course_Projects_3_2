import javax.servlet.Servlet;
import javax.servlet.ServletConfig;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.io.PrintWriter;

public class Sss extends HttpServlet implements Servlet {
    public Sss(){
        super();
        System.out.println("Sss:constructor");
    }

    public void init(ServletConfig sc) throws ServletException{
        super.init();
        System.out.println("Sss:init");
    }

    public void destroy(){
        super.destroy();
        System.out.println("Sss:destroy");
    }

   public void service(HttpServletRequest rq, HttpServletResponse rs)
            throws ServletException, IOException{
        super.service(rq, rs);
        System.out.println("Sss:service: "+rq.getMethod());
    }

    protected void doGet(HttpServletRequest rq, HttpServletResponse rs)
            throws ServletException, IOException {
        rs.setContentType("text/html");
        PrintWriter pw=rs.getWriter();
        pw.println("<html>" +
                "<body>Sss:doGet: " +rq.getMethod()+
                "<br>Sss:service:servername: "+rq.getServerName()+
                "<br>Sss:service:id: "+rq.getRemoteHost()+
                "<br>firstname: "+rq.getParameter("firstname")+
                "<br>lastname: "+rq.getParameter("lastname")+
                "<br>querystring: "+rq.getQueryString()+"</body></html>");
        pw.close();
    }

    protected void doPost(HttpServletRequest rq, HttpServletResponse rs) throws IOException, ServletException {
        rs.setContentType("text/html");
        PrintWriter pw=rs.getWriter();
        pw.println("<html>" +
                "<body>It is  " +rq.getMethod()+
                "<br>firstname: "+rq.getParameter("firstname")+
                "<br>lastname: "+rq.getParameter("lastname")+"</body></html>");
        pw.close();
    }
}
