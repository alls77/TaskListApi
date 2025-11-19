// Usage: mongosh --quiet --file 003_seed_tasklists.js

(function () {
    const DEFAULT_DB = (typeof DB_NAME !== "undefined") ? DB_NAME : "tasklistsdb";
    const collName = "tasklists";
    const dbRef = db.getSiblingDB(DEFAULT_DB);

    print(`Using database: ${DEFAULT_DB}`);

    if (dbRef.getCollectionNames().indexOf(collName) === -1) {
        throw new Error(`Collection '${collName}' does not exist. Run 001 script first.`);
    }

    const coll = dbRef.getCollection(collName);

    const now = new Date();

    const docs = [
        {
            Name: "Work",
            OwnerId: "user-1",
            CreatedAt: new Date(now.getTime() - 1000 * 60 * 60),
            SharedWith: []
        },
        {
            Name: "Personal",
            OwnerId: "user-2",
            CreatedAt: new Date(now.getTime() - 1000 * 60 * 60 * 2),
            SharedWith: ["user-1"]
        }
    ];

    docs.forEach(function (doc) {
        const filter = { OwnerId: doc.OwnerId, Name: doc.Name };
        const update = { $setOnInsert: doc };
        const res = coll.updateOne(filter, update, { upsert: true });
        if (res.upsertedId) {
            print(`Inserted seed doc (${doc.OwnerId} / ${doc.Name}) - _id: ${res.upsertedId._id}`);
        } else {
            print(`Seed doc already exists (${doc.OwnerId} / ${doc.Name}).`);
        }
    });

    print("Seeding finished.");
})();
