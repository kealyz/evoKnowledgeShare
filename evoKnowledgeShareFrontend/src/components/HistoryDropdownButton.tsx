import React, { useEffect, useState } from "react";
import { ButtonGroup, Dropdown, DropdownButton, Nav } from "react-bootstrap";
import { useNavigate } from "react-router-dom";

export default function HistoryDropdownButton(){
    const data = [
        {
            id:"1",
            version:"1.1"
        },
        {
            id:"2",
            version:"1.2"
        },
        {
            id:"3",
            version:"1.3"
        },
        {
            id:"4",
            version:"1.4"
        },
    ]

    // const [historyArray, setHistoryArray] = useState()
    // useEffect(() => {
    //     fetch('http://localhost:5145/api/History/id')
    //         .then(res => res.json())
    //         .then(json => {
    //             setHistoryArray(json)
    //         })
    // }, [])
    // console.log(data.length)
    let navigate = useNavigate();
    return (
        <DropdownButton as={ButtonGroup} title={data[0].version} key="Success" variant="success" id="1">
        {data.map((x, i) =>
            <Dropdown.Item eventKey={i} onClick={e => navigate(x.version)}>{x.version}</Dropdown.Item>
         )}
        </DropdownButton>
    )
}