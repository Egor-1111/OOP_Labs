using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOP_Lab2
{
    public class EventManager
    {
        List<User> listeners = new List<User>();
        
        
        public void Subscribe(User user)
        {
            listeners.Add(user);
        }
        public void Notify(string info)
        {
            foreach(User usr in listeners)
            {
                usr.Logs += info;
            }
        }
    }
}