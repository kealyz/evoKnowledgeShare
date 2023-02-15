import { motion } from 'framer-motion';
import { useEffect, useState } from 'react'
import RenderTable from '../components/ObjectTable';
import IDocument from '../interfaces/IDocument';

export const Notes = () => {
  //const dispatch = useDispatch();
  //let modalIsShown = useSelector((state: RootState) => state.modal.show);
  //const modalContent = useSelector((state: RootState) => state.modal.content);
  const [notes, setNotes] = useState<IDocument[]>([])
  //const [noteId, setNoteId] = useState<string>();

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
    setNotes(await getAllNotes.json());
  };

  useEffect(() => {
    fetchNotes();
  }, [])

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
        <RenderTable data={notes} />
      </motion.div>
    </>
  )
}
