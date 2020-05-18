package filters;

import javax.servlet.*;
import java.io.IOException;

public class F1 implements Filter {

    public void init(FilterConfig filterConfig) throws ServletException {
        System.out.println("F1:init");
    }

    public void doFilter(ServletRequest servletRequest, ServletResponse servletResponse, FilterChain filterChain) throws IOException, ServletException {
        System.out.println("F1:doFilter");
        filterChain.doFilter(servletRequest, servletResponse);
    }

    public void destroy() {
        System.out.println("F1:destroy");
    }
}
