| Users   | Get                                  | Post                     | Delete              | Put                  |
|---------|--------------------------------------|--------------------------|---------------------|----------------------|
|         | api/User/Users                       | api/User/UserRange/{ids} | api/User/Delete     | api/User/Update      |
|         | api//User/User/{id}                  | api/User/Create          | api/User/Delete{id} | api/User/UpdateRange |
|         |                                      |                          |                     |                      |
| Notes   |                                      |                          |                     |                      |
|         | api/Note/                            | api/Note/                | api/Note/           | api/Note/            |
|         | api/Note/{noteId}                    | api/Note/createRange     | api/Note/byId/{id}  |                      |
|         | api/Note/byUserId/{userId}           |                          |                     |                      |
|         | api/Note/byTopicId/{topicId}         |                          |                     |                      |
|         | api/Note/byDescription/{description} |                          |                     |                      |
|         | api/Note/byTitle/{title}             |                          |                     |                      |
|         |                                      |                          |                     |                      |
| Topic   |                                      |                          |                     |                      |
|         | api/Topic/Topics                     | api/Topic/Create         | api/Topic/Delete    |                      |
|         | api/TopicID/{id}                     |                          |                     |                      |
|         |                                      |                          |                     |                      |
| History |                                      |                          |                     |                      |
|         | api/History/Histories                | api/History/Create       |                     |                      |
|         | api/History/History/{id}             |                          |                     |                      |


## ActionResults

CREATE -> Created

UPDATE -> OK

DELTE -> NoContent

GET -> OK