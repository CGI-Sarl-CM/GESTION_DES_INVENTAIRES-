using System;
using System.Collections.Generic;
using System.Text;

namespace MyStoreData;



public interface IRealmFactory
{
    Realm GetRealmInstance();
}

public class RealmFactory : IRealmFactory
{
    private readonly RealmConfiguration _config;

    public RealmFactory()
    {
        
        // code de reference du chemin vers le dossier de la bd .
        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MyNoteRealm");
        //  Create database directory , creation du dossier de la base de données s'il n'existe pas .
        if (!Directory.Exists(dbPath))
        {
            Directory.CreateDirectory(dbPath);
        }

        // code de reference du chemin vers le dossier de la bd de log .(savoir si c'est en mode degage ou pas)
#if RELEASE
        string filePath = Path.Combine(dbPath, "MyNote.realm");

#elif DEBUG
        string filePath = Path.Combine(dbPath, "MyNoteDebug.realm");
       

#endif
     
        // Set schema version to 5.
        _config = new RealmConfiguration(filePath)
        {
            SchemaVersion = 02,

            MigrationCallback = (migration, oldSchemaVersion) =>
            {

            }
        };
        
        }
    
    public Realm GetRealmInstance()
    {
        return Realm.GetInstance(_config);
    }

    

}

