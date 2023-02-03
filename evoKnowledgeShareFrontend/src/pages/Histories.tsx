import { motion } from 'framer-motion'
import React, { useEffect, useState } from 'react'
import RenderTable from '../components/ObjectTable'
import IHistory from '../interfaces/IHistory'

export const Histories = () => {
  const [histories, setHistories] = useState<IHistory[]>([])

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

  useEffect(() => {
    fetch('https://localhost:5145/api/History')
      .then(res => res.json())
      .then(json => {
        setHistories(json)
      })
  }, [])

  function GetTopics() {
    return (
      <>
        {histories?.map((history) => {
          return (<p key={history.id}>{history.id}: {history.activity}</p>)
        })}
      </>
    )
  }

  return (
    <>
      <motion.div variants={containerVariant}
        initial="hidden"
        animate="visible">
        <RenderTable topics={histories} />
      </motion.div>
    </>
  )
}
