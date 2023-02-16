import { Table } from 'react-bootstrap';
import IObject from '../interfaces/IObject';
import '../css/Buttons.css';

interface RenderTableProps {
    data: IObject[],
    onDelete?: (guid: string) => void,
    onEdit?: (guid: string) => void
}

function GetActionButtons(guid: string, props: RenderTableProps) {
    //console.log(guid);
    //console.log(props)
    return (
        <div>
            {props.onEdit && <button className="function-buttons warning" onClick={() => props.onEdit && props.onEdit(guid)}>Edit</button>}
            {props.onDelete && <button className="function-buttons danger" onClick={() => props.onDelete && props.onDelete(guid)}>Delete</button>}
        </div>
    );
}

export default function OldRenderTable(props: RenderTableProps): JSX.Element {
    let columns: string[] = [];
    if (props.data) {
        for (let i in props.data[0]) {
            columns.push(i)
        }
    }

    return (
        <div>
            <Table striped bordered hover>
                <thead>
                    <tr>
                        {columns.map((header: string) => <th key={header}>{header.charAt(0).toUpperCase() + header.slice(1)}</th>)}
                        {(props.onDelete || props.onEdit) && <th>Actions</th>}
                    </tr>
                </thead>
                <tbody>
                    {props.data && props.data.map((row: IObject) =>
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