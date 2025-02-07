using System.Collections.Generic;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{
   private static NotificationManager _instance;
   public static NotificationManager Instance
   {
      get
      {
         if (_instance)
         {
            return _instance;
         }
         else
         {
            _instance = new GameObject("NotificationManager").AddComponent<NotificationManager>();
            return _instance;
         }
      }

      set
      {
         _instance = value;
      }
   }

   private void Awake() 
   {
      if(_instance == null) 
      {
         _instance = this;
         DontDestroyOnLoad(this);
      }
      else 
      {
         DestroyImmediate(this);
      }
   }

   private Dictionary<string, List<Component>> mainList = new Dictionary<string, List<Component>>();

   /* 
   _lName stands for the name of the list of components
   _c stands for the name of the component to be added to the list
   */
   public void AddListener(Component _component, string _listName) 
   {
      if(!mainList.ContainsKey(_listName)) 
      {
         mainList.Add(_listName, new List<Component>());
      }

      mainList[_listName].Add(_component);

      Debug.Log("Added " + _component.name + " to " + _listName);    
   }

   /*
   _cID stands for instance ID, as we are using the instance ID 
   matching and verifying and removing the component from 
   the list
   _lName stands for the name of the list of components
   */
   public void RemoveListener(int _componentID, string _listName)
   {
      if(mainList.ContainsKey(_listName)) 
      {
         foreach(Component _c in mainList[_listName])
         {   
            if(_c.GetInstanceID() == _componentID) 
            {
               mainList[_listName].Remove(_c);
            }
         }
      }

      //Redundancy checking
      List<Component> tempList = new List<Component>();

      foreach (Component _c in mainList[_listName])
      {
         if(_c != null) 
         {
            tempList.Add(_c);
         }
      }

      mainList[_listName] = tempList;
   }

   /*
   _funcName stands for the name of the function to be called on the listeners
   _lName stands for the name of the list of components
   _sendToChildren says if we are going to use SendMessage() function or BroadcastMessage() function, as the latter sends the message to all the children gameobjects as well.
   */
   public void SendNotification(string _functionName, string _listName, bool _sendToChildren = false) 
   {
      if(mainList.ContainsKey(_listName)) 
      {
         foreach (Component _c in mainList[_listName])
         {
            if(!_sendToChildren) 
            {
               _c.SendMessage(_functionName, SendMessageOptions.DontRequireReceiver);
               Debug.Log("Sent " + _functionName + " to " + _c.name);
            }
            else 
            {
               _c.BroadcastMessage(_functionName, SendMessageOptions.DontRequireReceiver);
            }
         }
      }
      else
      {
         Debug.LogError("List with the specified name " + _listName + " does not exist");
      }
   }

   /**/
   public void ClearList(string _listName) 
   {
      mainList[_listName].Clear();

      //Redundancy Check

      Dictionary<string, List<Component>> temp = new Dictionary<string, List<Component>>();

      foreach (KeyValuePair<string, List<Component>> item in mainList)
      {
         if(item.Value.Count > 0) 
         {
            temp.Add(item.Key, item.Value);
         }
      }

      mainList = temp;
   }

   public void RemoveAll()
   {
	   mainList.Clear();
   }
}
