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
        const data = async () => {
          const res = await fetch('https://localhost:5145/api/Topic/All');
          const json = await res.json();
          setTopics(json);
        }
        data();
      }, [])

    
    return (
        <>
            <motion.div variants={containerVariant}
                initial="hidden"
                animate="visible">
                <h1 className='mb-4'>Topic operations</h1>
                <RenderTable topics={topics} viewable={true} editable={true} deletable={false} />
            </motion.div>
        </>
    )
}