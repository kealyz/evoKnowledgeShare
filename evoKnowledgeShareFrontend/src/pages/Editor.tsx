import MDEditor from '@uiw/react-md-editor'
import { motion } from 'framer-motion'
import React from 'react';
import { useEffect, useState } from 'react'
import { Button, Form } from 'react-bootstrap';
import { useDispatch, useSelector } from 'react-redux';
import { useNavigate, useSearchParams } from 'react-router-dom';
import IEditorTopic from '../interfaces/IEditorTopic';
import moment from 'moment';
import { RootState } from '../store';
import { modalActions } from '../store/modal';
import { Modal } from '../ui/Modal';
import classes from './Editor.module.css';

export const Editor = () => {
  const [save, setSave] = useState<boolean>(false);
  const navigate = useNavigate();
  const [searchParams] = useSearchParams();
  const dispatch = useDispatch();
  let modalIsShown = useSelector((state: RootState) => state.modal.show);
  const modalContent = useSelector((state: RootState) => state.modal.content);

  const [value, setValue] = useState<string>("");
  const [documentName, setDocumentName] = useState<string>("");
  const [description, setDescription] = useState<string>("");
  const [level, setLevel] = useState<string>("");

  const [isLevelInvalid, setIsLevelInvalid] = useState<boolean>(false);
  const [isTopicTitleInvalid, setIsTopicTitleInvalid] = useState<boolean>(false);
  const [isValueInvalid, setIsValueInvalid] = useState<boolean>(false);
  const [documentNameInvalid, setDocumentNameInvalid] = useState<boolean>(false);
  const [topics, setTopics] = useState([] as IEditorTopic[]);

  const [topicId, setTopicId] = useState<string>("Choose a Topic Title");
  const [topicTitle, setTopicTitle] = useState<string>("");
  useEffect(() =>{
    fetch('https://localhost:5145/api/Topic/All')
            .then(res => res.json())
            .then(json => {
                setTopics(json)
            });
  }, [])

  useEffect(() => {
        topics.map(topic => { if (topic.id == searchParams.get("topic")) { setTopicTitle(topic.title); setTopicId(topic.id);} });
  }, [topics])

  useEffect(() => {
    if (value.trim().length < 5) {
      setIsValueInvalid(true);
    } else {
      setIsValueInvalid(false);
    }

    if (documentName.trim().length < 5 && documentName.trim().length === 0) {
      setDocumentNameInvalid(true)
    } else {
      setDocumentNameInvalid(false);
    }

    if (level === "1" || level === "2" || level === "3") {
      setIsLevelInvalid(false);
    } else {
      setIsLevelInvalid(true);
    }

    if (topicId === "Choose a Topic Title") {
      setIsTopicTitleInvalid(true);
    } else {
      setIsTopicTitleInvalid(false);
    }


  }, [value, documentName, level, topicId])

  const showModalHandler = (content: string) => {
    dispatch(modalActions.setContent(content))
    dispatch(modalActions.toggleShow())
  }

  const hideModalHandler = () => {
    dispatch(modalActions.toggleShow());
    dispatch(modalActions.removeModalContent);
  }

  //Basic form validation
  useEffect(() => {
    if (value.trim().length < 5) {
      setIsValueInvalid(true);
    } else {
      setIsValueInvalid(false);
    }

    if (documentName.trim().length < 5 && documentName.trim().length === 0) {
      setDocumentNameInvalid(true)
    } else {
      setDocumentNameInvalid(false);
    }

    if (level === "1" || level === "2" || level === "3") {
      setIsLevelInvalid(false);
    } else {
      setIsLevelInvalid(true);
    }

  }, [value, documentName, level])

  const onChangeValue = (e: any): void => {
    setValue(e);
  }

  const saveNoteRequest = async () => {
    const body = {
      note: {
        noteId: '3fa85f64-5717-4562-b3fc-2c963f66afa6',
        userId: '3fa85f64-5717-4562-b3fc-2c963f66afa6',
        topicId: '3fa85f64-5717-4562-b3fc-2c963f66afa6',
        createdAt: moment().format('YYYY-MM-DD[T]HH:mm:ss.SSSZ'),
        description: description,
        title: documentName
      },
      mdRaw: value,
      incrementSize: level
    }

    const request = await fetch('https://localhost:7145/api/Note', {
      method: 'POST',
      body: JSON.stringify(body),
      headers: {
        'Content-Type': 'application/json'
      }
    }
    );
    const result = await request.json();
    hideModalHandler();
    setSave(false);
  }

  const onSave = (e: React.MouseEvent<HTMLButtonElement, MouseEvent>) => {
    if (isLevelInvalid === false && isValueInvalid === false && documentNameInvalid === false) {
      showModalHandler("Saving...");
      setSave(true);
      saveNoteRequest()
      if(!save){
        navigate("/Notes");
      }

    } else {
      showModalHandler("Please check every fields")
    }
    e.preventDefault();
  }

  return (
    <>
      {modalIsShown && (
        <Modal onClose={hideModalHandler}>
          <div className="">
            <h2>{modalContent}</h2>

          </div>
        </Modal>
      )}

      <motion.div
        initial={{ opacity: 0, scale: 0.2 }}
        animate={{ opacity: 1, scale: 1 }}
        transition={{ duration: 0.7 }}
        className="container mt-5"
        data-color-mode="light">

        <div>
          <Form.Group className="mb-3">
            <div className={classes.topicItem}>
              <Form.Select aria-label="Select title" isInvalid={isTopicTitleInvalid} value={topicId} onChange={(e) => {setTopicId(e.target.value);}}>
                  <option>Choose a Topic Title</option>
                  {topics.map(topic => <option value={topic.id}>{topic.title}</option>)}
              </Form.Select>
            </div>
            <div className={classes.descriptionItem}>
              <Form.Control type="text" isInvalid={documentNameInvalid} placeholder="Enter the document name" value={documentName} onChange={(e) => setDocumentName(e.target.value)} />
            </div>
            <div className={classes.levelItem}>
              <Form.Select aria-label="Select change level" isInvalid={isLevelInvalid} value={level} onChange={(e) => setLevel(e.target.value)}>
                <option>Select change level</option>
                <option value="1">Small</option>
                <option value="2">Medium</option>
                <option value="3">Large</option>
              </Form.Select>
            </div>
            <div className={classes.buttonItem}><Button disabled={isValueInvalid} onClick={(e) => onSave(e)}>Save</Button></div>
          </Form.Group>
        </div>
        <Form.Group className="mb-3">
          <Form.Control type="text" placeholder="Enter the document description" value={description} onChange={(e) => setDescription(e.target.value)} />
        </Form.Group>
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