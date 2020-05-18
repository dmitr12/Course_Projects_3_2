package listeners;

import javax.servlet.ServletContext;
import javax.servlet.http.HttpSessionAttributeListener;
import javax.servlet.http.HttpSessionBindingEvent;

public class L2 implements HttpSessionAttributeListener {

    public void attributeAdded(HttpSessionBindingEvent httpSessionBindingEvent) {
        System.out.println("L2:attributeAdded:AttributeName="+httpSessionBindingEvent.getName());
        System.out.println("L2:attributeAdded:AttributeValue="+httpSessionBindingEvent.getValue());
    }

    public void attributeRemoved(HttpSessionBindingEvent httpSessionBindingEvent) {
        System.out.println("Lst2:attributeRemoved:AtributeName="   +httpSessionBindingEvent.getName());
        System.out.println("Lst2:attributeRemoved:AtributeValue="  +httpSessionBindingEvent.getValue());
    }

    public void attributeReplaced(HttpSessionBindingEvent httpSessionBindingEvent) {
        System.out.println("Lst2:attributeReplaced:AtributeName="  +httpSessionBindingEvent.getName());
        System.out.println("Lst2:attributeReplaced:AtributeOldValue "  +httpSessionBindingEvent.getValue());
        System.out.println("Lst2:attributeReplaced:AtributeNewValue "  +httpSessionBindingEvent.getSession().getAttribute(httpSessionBindingEvent.getName()));
    }
}
