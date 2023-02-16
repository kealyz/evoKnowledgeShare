import { motion } from 'framer-motion';
import { useEffect, useState } from 'react'
import { Accordion, Button, Card, ListGroup } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import RenderTable from '../components/ObjectTable';
import OldRenderTable from '../components/OldRenderTable';
import IDocument from '../interfaces/IDocument';

const containerVariant = {
  hidden: {
    opacity: 0
  },
  visible: {
    opacity: 1,
    transition: {
      duration: 0.2
    }
  }
}

export const Notes = () => {
  //const dispatch = useDispatch();
  //let modalIsShown = useSelector((state: RootState) => state.modal.show);
  //const modalContent = useSelector((state: RootState) => state.modal.content);
  const [notes, setNotes] = useState<IDocument[]>([])
  const navigate = useNavigate();
  //const [noteId, setNoteId] = useState<string>();

  const containerVariant = {
    hidden: {
      opacity: 0
    },
    visible: {
      opacity: 1,
      transition: {
        duration: 0.4
      }
    }
  }

  /*
  const showModalHandler = (content: string) => {
    dispatch(modalActions.setContent(content))
    dispatch(modalActions.toggleShow())
  }
 
  const hideModalHandler = () => {
    dispatch(modalActions.toggleShow());
    dispatch(modalActions.removeModalContent);
  }
*/

  const fetchNotes = async () => {
    const getAllNotes = await fetch('https://localhost:7145/api/Note');
    const fetchedNotes = await await getAllNotes.json()
    setNotes(fetchedNotes);
  };

  useEffect(() => {
    fetchNotes();
  }, [])

  const onEditHandle = async (noteId: string) => {
    console.log(notes)
    console.log(noteId)
  }

  const onOpenNote = (noteId: string) => {
    navigate(`/UpdateNote/${noteId}`);
    console.log(noteId);
  }

  return (
    <>
      {/*modalIsShown && (
          <Modal onClose={hideModalHandler}>
            <div className="">
              <p>{modalContent}</p>
              <Button className='m-2' variant="success" onClick={}>Yes</Button>
              <Button variant="danger" onClick={hideModalHandler}>No</Button>
            </div>
  
          </Modal>
        )*/}

      <motion.div variants={containerVariant}
        initial="hidden"
        animate="visible">
        <h1 className='mb-4'>Notes</h1>

        <motion.div variants={containerVariant}
          initial="hidden"
          animate="visible"></motion.div>
        {notes.map((entry) => (
          <Card className='m-4'>
            <Card.Header as="h6">{entry.createdAt.slice(0, 10)}</Card.Header>
            <Card.Body>
              <Card.Title>{entry.title}</Card.Title>
              <Card.Text>
                {entry.description}
              </Card.Text>
              <Button variant="primary" onClick={() => onOpenNote(entry.noteId)}>Open</Button>
            </Card.Body>
          </Card>
        ))}

      </motion.div>
    </>
  )
}
