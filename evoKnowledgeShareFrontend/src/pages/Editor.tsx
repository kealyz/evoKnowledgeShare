import MDEditor from '@uiw/react-md-editor'
import { motion } from 'framer-motion'
import { useState } from 'react'
import { Button } from 'react-bootstrap';
import { useDispatch, useSelector } from 'react-redux';
import { RootState } from '../store';
import { modalActions } from '../store/modal';
import { Modal } from '../ui/Modal';


export const Editor = () => {

  const [value, setValue] = useState("");

  const dispatch = useDispatch();
  const modalIsShown = useSelector((state: RootState) => state.modal.show);
  const modalContent = useSelector((state: RootState) => state.modal.content);

  const setModalContent = (props: string) => {
    dispatch(modalActions.setContent(props))
  }

  const showModalHandler = () => {
    setModalContent("Modal test")
    dispatch(modalActions.toggleShow())
  }

  const hideModalHandler = () => {
    dispatch(modalActions.toggleShow());
    dispatch(modalActions.removeModalContent);
  }

  const onChange = (e: any): void => {
    //console.log(e);
    setValue(e);
  }

  return (
    <>
      {modalIsShown && (
        <Modal onClose={hideModalHandler}>
          <p>{modalContent}</p>
        </Modal>
      )}

      <motion.div
        initial={{ opacity: 0, scale: 0.2 }}
        animate={{ opacity: 1, scale: 1 }}
        transition={{ duration: 0.7 }}
        className="container mt-5"
        data-color-mode="light">
        <MDEditor
          height={500}
          value={value}
          onChange={onChange}
        />
        <Button onClick={showModalHandler}>Modal test</Button>
      </motion.div>
    </>
  )
}
