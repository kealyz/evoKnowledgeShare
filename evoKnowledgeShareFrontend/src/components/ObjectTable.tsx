import { Button, Table } from 'react-bootstrap';
import { BsTrashFill } from 'react-icons/bs';
import { BsPencil } from 'react-icons/bs';
import { BsEye } from 'react-icons/bs';
import IObject from '../interfaces/IObject';

import '../css/Buttons.css';
import ITopic from '../interfaces/ITopic';
import React from 'react';

/*function instanceOfTopic(object: any): object is ITopic {
    type asd = keyof ITopic;
    asd
    Object.entries(ITopic).map((value: [string, any]) => {
        if(value[0] in ITopic)
    })
}*/

function RemoveContent(guid: string, type?: string) {
    alert("Removing data with id [" + guid + "]");
    let url = "api/RemoveById/?id=" + guid;
    //fetch(url, { method: 'DELETE' });
    //window.location.reload(false);
}

function UpdateContent(guid: string) {
    alert("Updating data with id [" + guid + "]");
    //url = url + "/DELETE/?ID=" + row;
    //fetch(url, { method: 'DELETE' });
    //window.location.reload(false);
}

function ViewContent(guid: string) {
    alert("Viewing data with id [" + guid + "]");
    //url = url + "/DELETE/?ID=" + row;
    //fetch(url, { method: 'DELETE' });
    //window.location.reload(false);
}

function GetActionButtons(guid: string) {
    return (
        <div>
            <button className="function-buttons info" onClick={() => ViewContent(guid)}>View</button>
            <button className="function-buttons warning" onClick={() => UpdateContent(guid)}>Edit</button>
            <button className="function-buttons danger" onClick={() => RemoveContent(guid,)}>Delete</button>
        </div>
    );
}

interface RenderTableProps {
    topics: IObject[]
}

export default function RenderTable(props: RenderTableProps): JSX.Element {
    console.log("got it")
    console.log(props.topics)

    let columns: string[] = [];
    if (props.topics) {
        for (let i in props.topics[0]) {
            columns.push(i)
        }
    }
  
    return (
        <div>
            <Table striped bordered hover>
                <thead>
                    <tr>
                        {columns.map((header: string) => <th key={header}>{header.charAt(0).toUpperCase() + header.slice(1)}</th>)}
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {props.topics.map((row: IObject) =>
                        <tr key={row.id}>
                            {Object.entries(row).map((value: [string, any]) => {
                                return (<td key={value[1]}>
                                    {value[1]}
                                </td>)
                            })}
                            <td>
                                {GetActionButtons(row.id)}
                            </td>
                        </tr>
                    )}
                </tbody>
            </Table>
        </div>
    );
}