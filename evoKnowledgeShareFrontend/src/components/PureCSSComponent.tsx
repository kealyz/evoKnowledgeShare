import React, { createElement, useEffect, useRef, useState } from "react";
import { Button } from "react-bootstrap";
import { useDispatch, useSelector } from "react-redux";
import { createRoutesFromChildren } from "react-router-dom";
import "../css/PureCSS.css"
import ITreeNode from "../interfaces/ITreeNode";
import { RootState } from "../store";
import { modalActions } from "../store/modal";
import { Modal } from "../ui/Modal"

export default function PureCSSComponent() {

    const data: ITreeNode[] = [
        {
            id: "1",
            name: "Topic: jók",
            isClosed: false,
            children: [
                {
                    id: "2",
                    name: "Note: nagyonjó",
                    isClosed: false

                },
                {
                    id: "3",
                    name: "Note: mégjobb",
                    isClosed: false

                }
            ]
        },
        {
            id: "4",
            name: "Topic: rosszak",
            isClosed: false,
            children: [
                {
                    id: "5",
                    name: "Note: rossz:(",
                    isClosed: false
                }
            ]
        },
        {
            id: "6",
            name: "Topic: meg",
            isClosed: false,
        }
    ]

    // const [topics, setTopics] = useState([] as ITreeNode[]);

    const [topics, setTopics] = useState(data as ITreeNode[]);

    useEffect(() => {
        fetch('http://localhost:5145/api/Topic/TreeView')
            .then(res => res.json())
            .then(json => {
                setTopics(json)
            })
    }, [])

    // font size

    function uncheckAll() {
        topics.forEach
        //document.querySelector('input[type="button]"')?.innerHTML.replace()
    }

    /*function uncheckAll(){
        document.querySelectorAll('ul').forEach(el => {
            if(el.className == "childke")
                el.remove()
    })
    }*/

    // const dispatch = useDispatch();
    // const modalIsShown = useSelector((state: RootState) => state.modal.show);
    // const modalContent = useSelector((state: RootState) => state.modal.content);

    // const setModalContent = (props: string) => {
    //     dispatch(modalActions.setContent(props))
    // }

    // const showModalHandler = () => {
    //     setModalContent("Add a Topic name: ")
    //     dispatch(modalActions.toggleShow())
    // }

    // const hideModalHandler = () => {
    //     dispatch(modalActions.toggleShow());
    //     dispatch(modalActions.removeModalContent);
    // }

    // {modalIsShown && (
    //     <Modal onClose={hideModalHandler}>
    //       <p>{modalContent}</p>
    //     </Modal>
    //     )}
    const topic_content = (topic: ITreeNode) => {
        if (topic.isClosed)
            return (<></>);
        return (
            <>
                {topic.hasOwnProperty("children") ?
                    <ul id={"ul" + topic} className="childke">
                        {topic.children?.map(note => <li key={note.id}><span className="tree_custom">{note.name}</span></li>)}
                        <li key="addNote">Create Note</li>
                    </ul> :
                    <ul>
                        <li key="addNote">Create Note</li>
                    </ul>}
            </>
        )
    }
    const changeIsClosed = (topic: ITreeNode, i: number) => {
        let newArray = [...topics]
        //newTopic.isClosed = !newTopic.isClosed.valueOf()
        topic.isClosed ? topic.isClosed = false : topic.isClosed = true
        newArray[i] = topic
        setTopics(newArray)
    }
    // onClick{e => navigate(node.name/Create)}
    return (
        <>
            <div>
                <button onClick={uncheckAll}>Close all</button>
            </div><ul className="tree">
                {topics.map((topic, i) => <li key={topic.name}>
                    <span onClick={e => changeIsClosed(topic, i)}>
                        {topic.name} ({topic.children?.length})
                    </span>
                    {topic_content(topic)}
                    </li>
                )}
                <li key="addTopic"><Button variant="success">Create Topic</Button></li>
            </ul>
        </>
    );
}
