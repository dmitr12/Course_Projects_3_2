package filters;

import javax.servlet.*;
import java.io.IOException;

public class F2 implements Filter {

    public void init(FilterConfig filterConfig) throws ServletException {
        System.out.println("F2:init");
    }

    public void doFilter(ServletRequest servletRequest, ServletResponse servletResponse, FilterChain filterChain) throws IOException, ServletException {
        System.out.println("F2:doFilter");
        filterChain.doFilter(servletRequest, servletResponse);
    }

    public void destroy() {
        System.out.println("F2:destroy");
    }
}
