import { motion } from 'framer-motion'
import React from 'react'

export const Home = () => {

  return (
    <motion.div initial={{ scale: 0, }}
      animate={{ rotate: 0, scale: 1 }}
      transition={{
        type: "spring",
        stiffness: 100,
        damping: 20
      }}>
      <h1>Home Page</h1>
    </motion.div>
  )
}
