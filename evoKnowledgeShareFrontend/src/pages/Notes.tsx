import { motion } from 'framer-motion';
import { useEffect, useState } from 'react'
import { Button, Card, Form } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
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
  const [notes, setNotes] = useState<IDocument[]>([])
  const [filterValue, setFilterValue] = useState<string>('');
  const navigate = useNavigate();

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
      <motion.div variants={containerVariant}
        initial="hidden"
        animate="visible">
        <h1 className='mb-4'>Notes</h1>
        <Form.Control type="text" placeholder="Search for title" value={filterValue} onChange={(e) => setFilterValue(e.target.value)} />
        <motion.div variants={containerVariant}
          initial="hidden"
          animate="visible"></motion.div>
        {notes && notes.filter(e => e.title.toLowerCase().includes(filterValue.toLowerCase())).map(entry  => (
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
