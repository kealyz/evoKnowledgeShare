import React, {useState} from 'react'
import IDocument from '../interfaces/IDocument';

export default function useLocalStorage(props:IDocument){
    let [elements, setElements] = useState(()=> {
        
    })



    return[elements, setElements];
}