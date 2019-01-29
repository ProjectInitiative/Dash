using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CommonLibrary.Database
{
    public class Database
    {
        Realm DB = null;

        public Database()
        {
            DB = Realm.GetInstance();
        }

        /// <summary>
        /// Writes a realm object to the realm database
        /// </summary>
        /// <param name="Object">RealmObject to be written to the Realm database</param>
        public void Write(RealmObject Object)
        {
            DB.Write(() =>
            {
                DB.Add(Object);
            });
            //DB.Dispose();
        }

        /// <summary>
        /// Obsolete with auto updating
        /// </summary>
        /// <param name="Object">RealmObject to be updated in the Realm database</param>
        public void Update(RealmObject Object)
        {
            DB.Write(() =>
            {
                DB.Add(Object, update: true);
            });
            //DB.Dispose();
        }

        /// <summary>
        /// Deletes the realm object given
        /// </summary>
        /// <param name="Object">RealmObject to be deleted from the Realm database</param>
        public void Delete(RealmObject Object)
        {
            if (Object != null)
            {
                DB.Write(() =>
                {
                    DB.Remove(Object);
                });
                //DB.Dispose();
            }
        }

        /// <summary>
        /// Deletes a RealmObject based on the ID and the type
        /// </summary>
        /// <param name="GUID">Unique ID of the RealmObject to be deleted</param>
        /// <param name="T">Type of the RealmObject to be deleted</param>
        public void Delete(string GUID, Type T)
        {
            Object Object = Find(GUID, T);
            if (Object != null)
            {
                DB.Write(() =>
                {
                    DB.Remove((RealmObject)Object);
                });
                //DB.Dispose();
            }
        }

        /// <summary>
        /// Deletes all objects in the realm database
        /// </summary>
        public void DeleteAll()
        {
            DB.Write(() =>
            {
                DB.RemoveAll();
            });
            //DB.Dispose();
        }

        /// <summary>
        /// Find a RealmObject from the ID and type
        /// </summary>
        /// <param name="GUID">Unique ID of the RealmObject to be found</param>
        /// <param name="T">Type of the RealmObject to be found</param>
        /// <returns></returns>
        public object Find(string GUID, Type T)
        {
            MethodInfo method = typeof(Realm).GetMethod("Find", new[] { typeof(string) });
            MethodInfo generic = method.MakeGenericMethod(T);
            return generic.Invoke(DB, new[] { GUID });
            
        }

        /// <summary>
        /// Returns all objects with a certain class name in the realm database 
        /// </summary>
        /// <param name="className">Name of the class</param>
        /// <returns></returns>
        public List<object> GetAllObjects(string className)
        {
            return DB.All(className).ToList();
        }

        /// <summary>
        /// Returns all objects in the realm database 
        /// </summary>
        /// <returns></returns>
        public List<RealmObject> GetAllObjects()
        {
            return DB.All<RealmObject>().ToList();
        }

        public IQueryable<object> GetIQueryable(string className)
        {
            return DB.All(className);
        }

        public IQueryable<object> GetIQueryable()
        {
            return DB.All<RealmObject>();
        }

        /// <summary>
        /// Returns a new GUID based on system parameters
        /// </summary>
        /// <returns></returns>
        public static string GUID()
        { 
            return Guid.NewGuid().ToString();
        }
    }
}
