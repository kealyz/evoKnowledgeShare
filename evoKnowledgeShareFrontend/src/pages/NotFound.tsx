import { motion } from 'framer-motion';
import React from 'react'
import { Image } from "react-bootstrap";
import photo from '../resources/NotFound.png'

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

export const NotFound = () => {
    return (
        <motion.div variants={containerVariant}
            initial="hidden"
            animate="visible">
            <Image style={{ width: "60%", marginLeft: "auto", marginRight: "auto", display: "block" }} src={photo} />
        </motion.div>
    )
}