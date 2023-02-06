import React, { useEffect, useState } from 'react';
import TreeView, { flattenTree } from 'react-accessible-treeview';
import { FaRegFolder, FaRegFolderOpen } from 'react-icons/fa';
import ITreeNode from '../interfaces/ITreeNode';
import "../css/AccessibleStyles.css";

//https://www.npmjs.com/package/react-accessible-treeview
export default function TreeViewComponent(){
        const folder = {
            name: "",
            children: [
                {
                    name: "src",
                    children: [{ name: "index.js" }, { name: "styles.css" }],
                },
                {
                    name: "node_modules",
                    children: [
                        {
                            name: "react-accessible-treeview",
                            children: [{ name: "index.js" }],
                        },
                        { name: "react", children: [{ name: "index.js" }] },
                    ],
                },
                {
                    name: ".npmignore",
                },
                {
                    name: "package.json",
                },
                {
                    name: "webpack.config.js",
                },
            ],
        };
    
        const [topics, setTopics] = useState<ITreeNode>(() => ({name:""}));
    
        useEffect(() => {
            fetch('http://localhost:5145/api/Topic/TreeView')
                .then(res => res.json())
                .then(json => {
                    setTopics(json)
                })
        }, [])
    
        const data = flattenTree(folder);
        const msot = flattenTree(topics);
    
        let treeLength = data.length-1;
        const expandedIds = Array.from(Array(treeLength), (_, index) => index + 1);

        const FolderIcon = ({ isOpen }) =>
            isOpen ? (
                <FaRegFolderOpen color="e8a87c" className="icon" />
            ) : (
                <FaRegFolder color="e8a87c" className="icon" />
            );
    
        return (
            <div>
                <div className="directory">
                    <TreeView
                        data={data}
                        aria-label="directory tree"
                        defaultExpandedIds={expandedIds}
                        nodeRenderer={({
                            element,
                            isBranch,
                            isExpanded,
                            isDisabled,
                            getNodeProps,
                            level,
                            handleExpand,
                        }) => (
                            <div {...getNodeProps({ onClick: handleExpand })} style={{ paddingLeft: 20 * (level - 1)}}>
                                {isBranch ? (
                                    <FolderIcon isOpen={isExpanded} />
                                ) : (
                                    <span></span>
                                )}
                                {element.children.length == 0 ? element.name : element.name + " " + element.children.length}
                            </div>
                        )}
                    />
                </div>
    
            </div>
    
        )
} 