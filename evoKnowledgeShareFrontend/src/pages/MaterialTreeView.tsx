import React, { useEffect, useState } from "react";
import TreeView from '@mui/lab/TreeView';
import TreeItem from '@mui/lab/TreeItem';
import Box from '@mui/material/Box';
import { FaRegFolder, FaRegFolderOpen } from 'react-icons/fa';
import Button from '@mui/material/Button';
import { useDispatch, useSelector } from "react-redux";
import { RootState } from "../store";
import { Modal } from "../ui/Modal"
import { modalActions } from "../store/modal";
import { useNavigate } from "react-router-dom";
import { Form } from "react-bootstrap";
import ITopicWithNotes from "../interfaces/TreeView/ITopicWithNotes";
import type {} from "@mui/lab/themeAugmentation"
import createTheme from "@mui/material/styles/createTheme";

const treeViewTheme = createTheme({
    components: {
        MuiTreeView: {
            styleOverrides: {
                root: {
                    paddingLeft: "55px",
                }
            }
        }
    }
})

const treeItemTheme = createTheme({
    components: {
        MuiTreeItem: {
            styleOverrides: {
                root: {
                    fontStyle: "italic",
                    transform: "scale(0.9)",
                },
            }
        }
    }
})

export default function MaterialTreeView() {
    const [topics, setTopics] = useState([] as ITopicWithNotes[]);

    useEffect(() => {
        fetch('https://localhost:5145/api/Topic/TreeView')
            .then(res => res.json())
            .then(json => {
                setTopics(json)
            });
    }, [])
    
    const parentIds: string[] = topics.map(node => node.notes?.length != 0 ? node.topicId : "");
    const [expanded, setExpanded] = useState<string[]>([]);
    const handleToggle = (event: React.SyntheticEvent, nodeIds: string[]) => {
        setExpanded(nodeIds);
    };

    const closeOrOpenTree = () => {
        setExpanded((oldExpanded) =>
            oldExpanded.length === 0 ? parentIds : []
        )
    }
    const renderTree = (nodes) => (
        <TreeItem key={nodes.topicId} nodeId={nodes.topicId} label={nodes.title}>
            {Array.isArray(nodes.notes
            )
                ? nodes.notes
                    .map((node) => renderTree(node))
                : null}
        </TreeItem>
    );

    /* Modal */
    const dispatch = useDispatch();
    let modalIsShown = useSelector((state: RootState) => state.modal.show);
    const modalContent = useSelector((state: RootState) => state.modal.content);

    const showModalHandler = () => {
        dispatch(modalActions.toggleShow())
    }

    const hideModalHandler = () => {
        dispatch(modalActions.toggleShow());
        dispatch(modalActions.removeModalContent);
    }

    const onSave = () => {
        showModalHandler()
        onSubmit(topicTitle)
        setTopicTitle("")
    }

    const getTreeAfterTopicAdd = () => {
        fetch('https://localhost:5145/api/Topic/TreeView')
            .then(res => res.json())
            .then(json => {
                setTopics(json)
        });
    }


    const onSubmit = async (topicTitle: string) => {
        const jsonObject = { title: topicTitle }
        const create = await fetch('https://localhost:7145/api/Topic/Create', { method: "POST", body: JSON.stringify(jsonObject), headers: { "Content-Type": "application/json" } })
        const response = await create.json()
        getTreeAfterTopicAdd();
    }

    const [topicTitle, setTopicTitle] = useState<string>("");
    let navigate = useNavigate();

    return (
        <>
            {modalIsShown && (
                <Modal onClose={hideModalHandler}>
                    <p>{modalContent}</p>
                    <Form.Control type="text" placeholder="Enter the Topic title" value={topicTitle} onChange={e => setTopicTitle(e.target.value)} />
                    <Button onClick={() => onSave()}>Save</Button>
                </Modal>
            )}
            <Box sx={{ height: 600, flexGrow: 1, maxWidth: 450, overflowY: 'auto' }}>
                <Box sx={{ mb: 1, paddingLeft: "55px" }}>
                    <Button onClick={closeOrOpenTree}>
                        {expanded.length === 0 ? 'Expand all' : 'Collapse all'}
                    </Button>
                </Box>
                <TreeView
                    aria-label="controlled"
                    expanded={expanded}
                    defaultExpanded={parentIds}
                    defaultCollapseIcon={<FaRegFolderOpen />}
                    defaultExpandIcon={<FaRegFolder />}
                    onNodeToggle={handleToggle}
                    theme={treeViewTheme}
                >
                    {topics.map(oneTopic =>
                        <TreeItem nodeId={oneTopic.topicId}  label={oneTopic.notes?.length != 0 ? 
                            oneTopic.title + " (" + oneTopic.notes?.length + ")" : oneTopic.title}>
                            {oneTopic.hasOwnProperty("notes") ? oneTopic.notes?.map(oneNote =>
                                <TreeItem nodeId={oneNote.noteId} label={oneNote.title} title={oneNote.createdAt.substring(0,10) + " " + oneNote.createdAt.substring(11,19)}></TreeItem>) : ""}
                            <TreeItem nodeId={"createNote" + oneTopic.topicId} label={"Create Note"} theme={treeItemTheme} onClick={e => navigate("../editor?topic=" + oneTopic.topicId)}></TreeItem>
                        </TreeItem>)}
                    <Button onClick={e => showModalHandler()}>Create Topic</Button>
                </TreeView>
            </Box>
        </>
    );
}