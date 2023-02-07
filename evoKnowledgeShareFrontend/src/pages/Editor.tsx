import MDEditor from '@uiw/react-md-editor'
import { motion } from 'framer-motion'
import React from 'react';
import { useState } from 'react'
import { Button, Form } from 'react-bootstrap';
import { useDispatch, useSelector } from 'react-redux';
import { useBeforeUnload, useLocation, useNavigate } from 'react-router-dom';
import { RootState } from '../store';
import { modalActions } from '../store/modal';
import { Modal } from '../ui/Modal';
import useLocalStorage from '../hooks/useLocalStorage';
import IDocument from '../interfaces/IDocument';


export const Editor = () => {

  const navigate = useNavigate();
  const dispatch = useDispatch();
  const location = useLocation();
  //TODO:Save and load in local storage => no need for a save button 
  //TODO: View needed ?
  //TODO: Are u sure / change
  const [elements, setElements] = useLocalStorage({id:'', title:'', content: '', version: ''});
  
  const [value, setValue] = useState<string>("");
  const [documentName, setDocumentName] = useState<string>("");
  let modalIsShown = useSelector((state: RootState) => state.modal.show);
  const modalContent = useSelector((state: RootState) => state.modal.content);

  const setModalContent = (props: string) => {
    dispatch(modalActions.setContent(props))
  }

  const showModalHandler = () => {
    setModalContent("Are you want to save this document?")
    dispatch(modalActions.toggleShow())
  }

  const hideModalHandler = () => {
    dispatch(modalActions.toggleShow());
    dispatch(modalActions.removeModalContent);
  }

  const onChangeValue = (e: any): void => {
    setValue(e);
  }

  const onSave = () => {
    //navigate('/')
    //alert('Saved');
    dispatch(modalActions.toggleShow());
  }

  //1. alaklami mentés
  //n. mentés
  //Give this a title
  //editor felett szürke new note input (unsaved)
  //minor change

  return (
    <>
      {modalIsShown && (
        <Modal onClose={hideModalHandler}>
          <div className="">
            <p>{modalContent}</p>
            <Button onClick={onSave} variant="success">Save</Button>
          </div>

        </Modal>
      )}
      
      <motion.div
        initial={{ opacity: 0, scale: 0.2 }}
        animate={{ opacity: 1, scale: 1 }}
        transition={{ duration: 0.7 }}
        className="container mt-5"
        data-color-mode="light">
        <Form.Group className="mb-3">
          <Form.Control type="text" placeholder="Enter the document name" value={documentName} onChange={(e) => setDocumentName(e.target.value)} />
        </Form.Group>
        {/*<Button className='mb-3' onClick={showModalHandler}>Save</Button>*/}
        <MDEditor
          height={450}
          value={value}
          preview="edit"
          onChange={onChangeValue}
        />
      </motion.div>
    </>
  )
}
