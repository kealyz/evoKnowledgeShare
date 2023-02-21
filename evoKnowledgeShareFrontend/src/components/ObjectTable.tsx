import { Button, Table } from 'react-bootstrap';
import { BsTrashFill } from 'react-icons/bs';
import { BsPencil } from 'react-icons/bs';
import { BsEye } from 'react-icons/bs';
import IObject from '../interfaces/IObject';

import '../css/Buttons.css';
import ITopic from '../interfaces/ITopic';
import { useLocation } from 'react-router-dom';
import FilterArrayFunction from './Filter';
import { useEffect, useState } from 'react';

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

    /*
    //BUG: ha túl sok az oszlop akkor az első betöltésnél nem jelenítit meg a táblát csak akkor ha a filter megváltozik, így folyamatosan
    useEffect(() => {
     handleFilter(filteredData)
     console.log(props.data)
    }, [filteredData, props.data])
    */
    
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