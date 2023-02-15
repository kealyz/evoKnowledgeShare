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

interface RenderTree {
    id: string;
    name: string;
    children?: readonly RenderTree[];
}

const data = [
    {
        id: "1",
        name: "Topic: jók",
        children: [
            {
                id: "2",
                name: "Note: nagyonjó",
            },
            {
                id: "3",
                name: "Note: mégjobb"
            }
        ]
    },
    {
        id: "4",
        name: "Topic: rosszak",
        children: [
            {
                id: "5",
                name: "Note: rossz:("
            }
        ]
    },
    {
        id: "6",
        name: "Topic: meg"
    }
]

let useEffectActivator = 1;
export default function MaterialTreeView() {

    
    const [topics, setTopics] = useState([]);

    useEffect(() => {
        fetch('https://localhost:5145/api/Topic/TreeView')
            .then(res => res.json())
            .then(json => {
                setTopics(json)
            });
    }, [useEffectActivator])

    const parentIds = topics.map(node => (node.id));

    const [expanded, setExpanded] = useState<string[]>([]);
    const handleToggle = (event: React.SyntheticEvent, nodeIds: string[]) => {
        setExpanded(nodeIds);
    };

    const closeOrOpenTree = () => {
        setExpanded((oldExpanded) =>
            oldExpanded.length === 0 ? parentIds : []
        )
    }
    const renderTree = (nodes: RenderTree) => (
        <TreeItem key={nodes.id} nodeId={nodes.id} label={nodes.name}>
            {Array.isArray(nodes.children)
                ? nodes.children.map((node) => renderTree(node))
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
        useEffectActivator += 1;
        setTopicTitle("")
    }


    const onSubmit = async (topicTitle: string) => {
        const jsonObject = {title: topicTitle}
        const create = await fetch('https://localhost:7145/api/Topic/Create', { method: "POST", body: JSON.stringify(jsonObject), headers: { "Content-Type": "application/json" } })
        const response = await create.json()
        console.log(useEffectActivator)
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
            <Box sx={{ height: 270, flexGrow: 1, maxWidth: 400, overflowY: 'auto' }}>
                <Box sx={{ mb: 1 }}>
                    <Button onClick={closeOrOpenTree}>
                        {expanded.length === 0 ? 'Expand all' : 'Collapse all'}
                    </Button>
                </Box>
                <TreeView
                    aria-label="controlled"
                    defaultExpanded={parentIds}
                    expanded={expanded}
                    defaultCollapseIcon={<FaRegFolderOpen />}
                    defaultExpandIcon={<FaRegFolder />}
                    onNodeToggle={handleToggle}
                >
                    {topics.map(node =>
                        <TreeItem nodeId={node.id} label={node.children?.length != undefined ? node.name + " (" + node.children?.length + ")" : node.name}>
                            {node.hasOwnProperty("children") ?
                                node.children?.map(level => 
                                <TreeItem nodeId={level.id} label={level.name}></TreeItem>)
                                : ""}
                            <TreeItem nodeId={"createNote" + node.id} label={"Create Note"} onClick={e => navigate("editor?topic=" + node.id)}></TreeItem> 
                        </TreeItem>)}
                    <Button onClick={e => showModalHandler()}>Create Topic</Button>

                </TreeView>
            </Box>
        </>
    );
}