import { motion } from "framer-motion";
import { useEffect, useState } from "react";
import { Button } from "react-bootstrap";
import { useDispatch, useSelector } from "react-redux";
import RenderTable from "../components/ObjectTable";
import ITopic from "../interfaces/ITopic";
import { RootState } from '../store'
import { modalActions } from "../store/modal";
import { Modal } from '../ui/Modal';


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

export default function Topics() {
    const [topics, setTopics] = useState<ITopic[]>([])
    const [topicId, setTopicId] = useState<string>();
    const dispatch = useDispatch();
    let modalIsShown = useSelector((state: RootState) => state.modal.show);
    const modalContent = useSelector((state: RootState) => state.modal.content);

    useEffect(() => {
        fetchTopics();
    }, [topics])

    const showModalHandler = (content: string) => {
        dispatch(modalActions.setContent(content))
        dispatch(modalActions.toggleShow())
    }

    const hideModalHandler = () => {
        dispatch(modalActions.toggleShow());
        dispatch(modalActions.removeModalContent);
    }

    const fetchTopics = async () => {
        const getAllTopics = await fetch('https://localhost:5145/api/Topic/All');
        setTopics(await getAllTopics.json())
    }

    const deleteTopic = async () => {
        console.log(topicId)
        const response = await fetch(`https://localhost:7145/api/Topic/Delete/${topicId}`, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json',
            },
        });
        setTopicId('');
        hideModalHandler();
    }


    const onDeleteTopic = async (topicId: string) => {
        showModalHandler("Are you want to delete " + topicId + " ?");
        setTopicId(topicId);
    }

    return (
        <>
            {modalIsShown && (
                <Modal onClose={hideModalHandler}>
                    <div className="">
                        <p>{modalContent}</p>
                        <Button className='m-2' variant="success" onClick={deleteTopic}>Yes</Button>
                        <Button variant="danger" onClick={hideModalHandler}>No</Button>
                    </div>

                </Modal>
            )}
            <motion.div variants={containerVariant}
                initial="hidden"
                animate="visible">
                <h1 className='mb-4'>Topic operations</h1>
                {(topics && Object.keys(topics).length !== 0) ? <RenderTable data={topics} onDelete={onDeleteTopic} /> : <h2>No topic</h2>}
            </motion.div>
        </>
    )
}