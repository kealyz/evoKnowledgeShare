import { motion } from "framer-motion";
import { useEffect, useState } from "react";
import RenderTable from "../components/ObjectTable";
import ITopic from "../interfaces/ITopic";


const containerVariant = {
    hidden: {
        opacity: 0
    },
    visible: {
        opacity: 1,
        transition: {
            duration: 0.2
        }
    }
}

export default function Topics() {
    const [topics, setTopics] = useState<ITopic[]>([])

    useEffect(() => {
        fetch('https://localhost:5145/api/Topic/Test')
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
            <motion.div variants={containerVariant}
                    initial="hidden"
                    animate="visible">
                <RenderTable topics={topics} />
            </motion.div>
        </>
    )
}