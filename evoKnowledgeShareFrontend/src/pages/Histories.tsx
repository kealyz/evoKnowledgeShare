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
    const data = async () => {
      const res = await fetch('https://localhost:5145/api/History');
      const json = await res.json();
      setHistories(json);
    }
    data();
  }, [])

  return (
    <>
      <motion.div variants={containerVariant}
        initial="hidden"
        animate="visible">
        <h1 className='mb-4'>History operations</h1>
        {(histories && Object.keys(histories).length !== 0) ? <RenderTable data={histories}/> : <h2>No histories</h2>}
      </motion.div>
    </>
  )
}