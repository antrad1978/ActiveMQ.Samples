using System;
using Apache.NMS;
using Apache.NMS.ActiveMQ;

namespace ActiveMQ.Base
{
    public class BaseConnection 
    { 
        //public const string URI = "activemq:tcp://localhost:61616"; 
        public const string URI = "activemq:tcp://localhost:61002"; 
        public static IConnectionFactory connectionFactory; 
        public static IConnection _connection; 
        public ISession _session; 
 
        public BaseConnection() 
        { 
            connectionFactory = new ConnectionFactory(URI); 
            if (_connection == null) 
            { 
                _connection = connectionFactory.CreateConnection(); 
                _connection.Start(); 
                _session = _connection.CreateSession(); 
            } 
        } 
    } 
}
