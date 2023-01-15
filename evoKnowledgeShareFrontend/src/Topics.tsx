import { useEffect, useState } from "react";
import RenderTable from "./components/ObjectTable";
import Table from "./components/ObjectTable";
import ITopic from "./interfaces/ITopic";

export default function Topics() {
    const [topics, setTopics] = useState<ITopic[]>([])

    useEffect(() => {
        fetch('http://localhost:5145/api/Topic/Test')
            .then(res => res.json())
            .then(json => {
                setTopics(json)
            })
    }, [])

    function GetTopics() {
        return (
            <>
                {topics?.map((topic) => {
                    return (<p key={topic.id}>{topic.id}: {topic.title}</p>)
                })}
            </>
        )
    }

    return (
        <>
            {<RenderTable topics={topics} />}
        </>
    )
}