// Usage: mongosh --quiet --file 001_create_tasklists_collection_with_validator.js
// The script will use the DB name set in the mongosh connection, or you can set DB_NAME env var and pass it via --eval "DB_NAME='tasklistsdb'"

(function () {
    const DEFAULT_DB = (typeof DB_NAME !== "undefined") ? DB_NAME : "tasklistsdb";
    const collName = "tasklists";
    const dbRef = db.getSiblingDB(DEFAULT_DB);

    print(`Using database: ${DEFAULT_DB}`);

    const validator = {
        $jsonSchema: {
            bsonType: "object",
            required: ["Name", "OwnerId", "CreatedAt"],
            properties: {
                Name: {
                    bsonType: "string",
                    minLength: 1,
                    maxLength: 255,
                    description: "must be a string between 1 and 255 chars"
                },
                OwnerId: {
                    bsonType: "string",
                    description: "ownerId must be a string"
                },
                CreatedAt: {
                    bsonType: "date",
                    description: "createdAt must be a date"
                },
                SharedWith: {
                    bsonType: ["array"],
                    items: { bsonType: "string" },
                    description: "sharedWith must be an array of strings"
                }
            }
        }
    };

    const collExists = dbRef.getCollectionNames().indexOf(collName) !== -1;

    if (!collExists) {
        print(`Collection '${collName}' does not exist. Creating with validator...`);
        try {
            dbRef.createCollection(collName, {
                validator: validator,
                validationAction: "error",
                validationLevel: "strict"
            });
            print(`Collection '${collName}' created with validator (strict).`);
        } catch (err) {
            print(`Error creating collection '${collName}': ${err}`);
            throw err;
        }
    } else {
        print(`Collection '${collName}' already exists. Applying/updating validator via collMod (moderate).`);
        try {
            const cmd = {
                collMod: collName,
                validator: validator,
                validationLevel: "moderate",
                validationAction: "error"
            };
            const res = dbRef.runCommand(cmd);
            print(`collMod result: ${tojson(res)}`);
        } catch (err) {
            print(`collMod failed: ${err}`);
            throw err;
        }
    }

    const collInfo = dbRef.getCollectionInfos({ name: collName })[0] || {};
    print("Current collection info:");
    printjson(collInfo.options && collInfo.options.validator ? collInfo.options.validator : "(no validator found)");
})();
