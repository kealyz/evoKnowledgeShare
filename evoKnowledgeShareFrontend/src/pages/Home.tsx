import { motion } from 'framer-motion'
import { Card } from 'react-bootstrap'
import classes from './Home.module.css'

export const Home = () => {

  return (
    <motion.div initial={{ scale: 0, }}
      animate={{ rotate: 0, scale: 1 }}
      transition={{
        type: "spring",
        stiffness: 100,
        damping: 20
      }}>

      <h1>Welcome in evoKnowledgeShare</h1>

      <div className="">
        <p>With this application you are able to make valuable notes to others and you can freely read and update others notes.</p>
      </div>

      <div>
        <p>If you are not familiar with markdown language please check this sources:</p>
        <div>
          <Card onClick={() => window.open("https://www.markdownguide.org/", "_blank")} className={classes.cardItem} style={{ width: 'auto' }}>
            <motion.div
              whileHover={{
                scale: 0.98,
                transition: { duration: 0.3 },
              }}>
              <Card.Body>
                <Card.Title>Markdown Guide</Card.Title>
                <Card.Subtitle className="mb-2 text-muted">Click on the card</Card.Subtitle>
                <Card.Text>
                  The Markdown Guide is a free and open-source reference guide that explains how to use Markdown, the simple and easy-to-use markup language you can use to format virtually any document.
                </Card.Text>
              </Card.Body>
            </motion.div>
          </Card>

          <Card onClick={() => window.open("https://daringfireball.net/projects/markdown/syntax", "_blank")} className={classes.cardItem} style={{ width: 'auto' }}>
            <motion.div whileHover={{
              scale: 0.98,
              transition: { duration: 0.3 },
            }}>
              <Card.Body>
                <Card.Title>Markdown: Syntax</Card.Title>
                <Card.Subtitle className="mb-2 text-muted">Click on the card</Card.Subtitle>
                <Card.Text>
                  Markdown is intended to be as easy-to-read and easy-to-write as is feasible.
                </Card.Text>
              </Card.Body>
            </motion.div>
          </Card>

          <Card onClick={() => window.open("https://github.com/adam-p/markdown-here/wiki/Markdown-Cheatsheet", "_blank")} className={classes.cardItem} style={{ width: 'auto' }}>
            <motion.div whileHover={{
              scale: 0.98,
              transition: { duration: 0.3 },
            }}>
              <Card.Body>
                <Card.Title>Markdown Cheatsheet</Card.Title>
                <Card.Subtitle className="mb-2 text-muted">Click on the card</Card.Subtitle>
                <Card.Text>
                  This is intended as a quick reference and showcase. For more complete info, see John Gruber's original spec and the Github-flavored Markdown info page.
                </Card.Text>
              </Card.Body>
            </motion.div>
          </Card>

        </div>
      </div>
    </motion.div>
  );
}
