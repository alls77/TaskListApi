// Usage: mongosh --quiet --file 002_create_tasklists_indexes.js

(function () {
    const DEFAULT_DB = (typeof DB_NAME !== "undefined") ? DB_NAME : "tasklistsdb";
    const collName = "tasklists";
    const dbRef = db.getSiblingDB(DEFAULT_DB);

    print(`Using database: ${DEFAULT_DB}`);

    if (dbRef.getCollectionNames().indexOf(collName) === -1) {
        throw new Error(`Collection '${collName}' does not exist. Run 001 script first.`);
    }

    const coll = dbRef.getCollection(collName);

    // ownerId index
    print("Creating index: { ownerId: 1 }");
    coll.createIndex({ OwnerId: 1 }, { name: "IX_OwnerId" });

    // createdAt descending
    print("Creating index: { createdAt: -1 }");
    coll.createIndex({ CreatedAt: -1 }, { name: "IX_CreatedAt_Desc" });

    // sharedWith index
    print("Creating index: { sharedWith: 1 }");
    coll.createIndex({ SharedWith: 1 }, { name: "IX_SharedWith" });

    print("Indexes created/ensured.");
})();
