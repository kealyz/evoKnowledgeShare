import { Button, Table } from 'react-bootstrap';
import { BsTrashFill } from 'react-icons/bs';
import { BsPencil } from 'react-icons/bs';
import { BsEye } from 'react-icons/bs';
import IObject from '../interfaces/IObject';

import '../css/Buttons.css';
import ITopic from '../interfaces/ITopic';
import { useLocation } from 'react-router-dom';
import FilterArrayFunction from './Filter';
import { useState } from 'react';

/*function instanceOfTopic(object: any): object is ITopic {
    type asd = keyof ITopic;
    asd
    Object.entries(ITopic).map((value: [string, any]) => {
        if(value[0] in ITopic)
    })
}*/

function RemoveContent(guid: string, type?: string) {
    //alert("Removing data with id [" + guid + "]");
    //let url = "api/"+location.pathname+"/Delete/"+guid;

    //console.log(location.pathname);
    //console.log(url)
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

interface RenderTableProps {
    data: IObject[],
    onDelete?: (guid: string) => void,
    onEdit?: (guid: string) => void
}

function GetActionButtons(guid: string, props: RenderTableProps) {
    return (
        <div>
            {props.onEdit && <button className="function-buttons warning" onClick={() => props.onEdit && props.onEdit(guid)}>Edit</button>}
            {props.onDelete && <button className="function-buttons danger" onClick={() => props.onDelete && props.onDelete(guid)}>Delete</button>}
        </div>
    );
}

export default function RenderTable(props: RenderTableProps): JSX.Element {
    let columns: string[] = [];
    if (props.data) {
        for (let i in props.data[0]) {
            columns.push(i)
        }
    }

    const [filteredData, setFilteredData] = useState<IObject[]>([]);

    const handleFilter = (data: IObject[]) => {
        setFilteredData(data);
    };

    return (
        <div>
            <FilterArrayFunction data={props.data} onFilter={handleFilter} />
            <Table striped bordered hover>
                <thead>
                    <tr>
                        {columns.map((header: string) => <th key={header}>{header.charAt(0).toUpperCase() + header.slice(1)}</th>)}
                        {(props.onDelete || props.onEdit) && <th>Actions</th>}
                    </tr>
                </thead>
                <tbody>
                    {filteredData.map((row: IObject) =>
                        <tr key={row.id}>
                            {Object.entries(row).map((value: [string, any]) => {
                                return (<td key={value[0]}>
                                    {value[1]}
                                </td>)
                            })}
                            {(props.onDelete || props.onEdit) &&
                                <td>

                                    {GetActionButtons(row.id, props)}
                                </td>
                            }
                        </tr>
                    )}
                </tbody>
            </Table>
        </div>
    );
}